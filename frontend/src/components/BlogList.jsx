import { useState, useEffect } from 'react'
import './BlogList.css'
import API from '../API'
import { message } from 'antd';
import { Avatar, Card } from 'antd';
const { Meta } = Card;
import { useNavigate } from 'react-router-dom'
import { DeleteOutlined } from '@ant-design/icons';
import { Flex, Rate,Button } from 'antd';
import { FrownOutlined, MehOutlined, SmileOutlined } from '@ant-design/icons';
import { MyContext } from '../App';
import { useContext } from 'react';

import { Popconfirm } from "antd";


const customIcons = {
    1: <FrownOutlined title="poor" />,
    2: <FrownOutlined title='average' />,
    3: <MehOutlined title='good' />,
    4: <SmileOutlined title='better' />,
    5: <SmileOutlined title='excellent' />,
};


function BlogList()
{
    const [admin, setAdmin] = useState(true);
    const [render,setRender]=useState(1);
    return (
        <div>
            {/* {admin && <UploadBlog/>} */}
            <AdminBlogs render={render} setRender={setRender} admin={true} />
            <AdminBlogs render={render} setRender={setRender} admin={false} />
        </div>
    )
}

function AdminBlogs({ render,admin,setRender })
{
    const navigate = useNavigate()
    const [blogs, setBlogs] = useState(null)
    const openBlog = (blogId) =>
    {
        //alert(blogId)
        let route = "/blogs/" + blogId
        //alert(route)
        navigate(route)
    }
    const deleteBlog = async (e, blog) =>
    {
        //alert("del blog clicked")
        //alert(blog)
        try
        {
            e.stopPropagation();
            //setBlogs(null)
            let call = await API('api/Blogs/removeblog?id=' + blog, { "credentials": "include", "method": "DELETE" });
            if (!call.ok)
            {
                throw new Error(await call.text())
            }
            // setBlogs((prev) =>
            // {
            //     const netresult = prev.filter((el) => el.id != blog);
            //     console.log(netresult)
            //     return netresult
            // });
            setRender(prev=>prev+1)
            message.success("deleted successfully.");

        }
        catch (err)
        {
            message.error(err.message)
        }

    }
    useEffect(() =>
    {
        (async function ()
        {
            try
            {
                let call = null
                if (admin)
                    call = await API('api/Blogs/all/admin', { "credentials": "include" })
                else
                    call = await API('api/Blogs/all', { "credentials": "include" })

                if (!call.ok)
                {
                    throw new Error(await call.text())
                }
                let adminblogs = await call.json();
                console.log(adminblogs)
                setBlogs(adminblogs)

            }
            catch (err)
            {
                message.error(err.message)
            }
        })()
    }, [render])

    //console.log(blogs)
    if (!blogs)
    {
        return <i>loading...</i>
    }
    return (
        <div className="admin-bloglist noto-sans-1">
            {/* {!blogs&&<i>loading...</i>} */}
            {admin && <h1>Your Blogs</h1>}
            {!admin && <h1>All Blogs</h1>}
            {blogs.length == 0 && <i><h1 style={{ color: "#b3b3b1", textAlign: 'center' }}>You Have No Blogs Published.</h1></i>}
            <div className='blog-grid'>
                {blogs.map((blog, ix) =>
                {
                    return <BlogCard render={render} setRender={setRender} deleteBlog={deleteBlog} onClick={() => openBlog(blog.id)} id={blog.id} blog={blog} key={ix} />
                })}
            </div>

        </div>
    )
}

//onClick={(e)=>deleteBlog(e,blog.id)}
function BlogCard({ render,setRender,deleteBlog, blog, onClick, id })
{
    const [rating,setRating]=useState(0);
    const {text,setText}=useContext(MyContext)
    //console.log("rendering");
    //console.log('rendering '+count)
    //count+=1
    useEffect(()=>{
        (async function(){
            try{
                let api=await API('api/Rating/getrating/'+id,{"credentials":"include"});
                if(!api.ok){
                    throw new Error(await api.text());
                }
                const jsonObj=await api.json();
                //console.log(jsonObj);
                //alert(jsonObj.rating)
                setRating(jsonObj.rating);
            }
            catch(err){
                message.error(err.message)
            }
        })()
    },[render])
    const giveRating = async (val) =>
    {

        try
        {
            let call = await API("api/Rating/addrating", {
                headers: {
                    'Accept': '*/*',
                    'Content-Type': 'application/json'
                },
                "method": "POST",
                "credentials": "include",
                "body": JSON.stringify({ rating: val, blogId: id })
            });
            if (!call.ok)
            {
                throw new Error(await call.text());
            }
            setRender(prev=>prev+1)
            message.success(await call.text())
            //setRating(val);
        }
        catch (err)
        {
            message.error(err.message);
        }
    }
    const confirm = (e) => {
        e.stopPropagation();
        console.log(e);
        message.success('Click on Yes');
      };
      

    return (
        <Card onClick={onClick} className="blog-card"

            cover={
                <img
                    alt="example"
                    src={blog.blogThumbnailImg}
                    width={300}
                />
            }
        // actions={[

        // ]}

        >
            <Meta
                style={{ fontSize: "30px" }}
                avatar={<Avatar src="https://api.dicebear.com/7.x/miniavs/svg?seed=8" />}
                title={blog.blogHeading}
                description=""
            />
            <i>published on {blog.publishDate}</i>
            <Flex gap="middle" style={{
                flexDirection: "row",
                justifyContent: "space-around",
                marginTop: "1rem"
            }} >
                <Rate value={rating} onChange={(val) => giveRating(val)} onClick={e => { e.stopPropagation(); }} defaultValue={rating} character={({ index = 0 }) => customIcons[index + 1]} />
                {/* <DeleteOutlined onClick={(e) => deleteBlog(e, blog.id)} title="delete blog" key="delete" style={{ color: "red", fontSize: "1rem" }} />, */}
                {text&&<Popconfirm
                    onClick={e => { e.stopPropagation(); }}
                    title="Delete the Blog"
                    description="Are you sure to delete this blog?"
                    onConfirm={(e)=>deleteBlog(e,blog.id)}
                    onCancel={(e)=>e.stopPropagation()}
                    okText="Yes"
                    cancelText="No"
                >
                    <Button style={{color:"white",fontWeight:"bold",backgroundColor:"rgb(223, 31, 59)"}} danger>Delete</Button>
                </Popconfirm>}
            </Flex>
        </Card >
    )
}


export default BlogList