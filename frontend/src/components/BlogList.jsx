import { useState, useEffect } from 'react'
import './BlogList.css'
import API from '../API'
import { message } from 'antd';
import { Avatar, Card } from 'antd';
const { Meta } = Card;
import { useNavigate } from 'react-router-dom'
import { DeleteOutlined } from '@ant-design/icons';
import { Flex, Rate } from 'antd';
import { FrownOutlined, MehOutlined, SmileOutlined } from '@ant-design/icons';

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
    return (
        <div>
            {/* {admin && <UploadBlog/>} */}
            <AdminBlogs admin={true} />
            <AdminBlogs admin={false} />
        </div>
    )
}

function AdminBlogs({ admin })
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
            setBlogs((prev) =>
            {
                const netresult = prev.filter((el) => el.id != blog);
                console.log(netresult)
                return netresult
            });

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
    }, [])

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
                    return <BlogCard deleteBlog={deleteBlog} onClick={() => openBlog(blog.id)} id={blog.id} blog={blog} key={ix} />
                })}
            </div>

        </div>
    )
}


//onClick={(e)=>deleteBlog(e,blog.id)}
function BlogCard({ deleteBlog, blog, onClick, id })
{
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
            message.success("ratind added successfully")
        }
        catch (err)
        {
            message.error(err.message);
        }
    }

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
                <Rate onChange={(val) => giveRating(val)} onClick={e => { e.stopPropagation(); }} defaultValue={3} character={({ index = 0 }) => customIcons[index + 1]} />
                <DeleteOutlined onClick={(e) => deleteBlog(e, blog.id)} title="delete blog" key="delete" style={{ color: "red", fontSize: "1rem" }} />,

            </Flex>
        </Card >
    )
}


export default BlogList