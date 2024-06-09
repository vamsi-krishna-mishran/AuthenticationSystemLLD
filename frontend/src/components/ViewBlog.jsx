import { useParams } from "react-router-dom";
import './ViewBlog.css'
import { useEffect,useState } from "react";
import { message } from "antd";
import API from '../API'

function ViewBlog() {
    const {blogId}=useParams()
    const [blog,setBlog]=useState(null)
    console.log("inside view blog")
    console.log(blogId)
    useEffect(()=>{
        (async function(){
            try{
                let call=await API("api/Blogs/"+blogId,{"credentials":"include"})
                if(!call.ok){
                    throw new Error(await call.text());
                }
                let blogobj=await call.json()
                setBlog(blogobj.blogDocument)
            }
            catch(err){
                message.error(err.message)
            }
        })()
    },[])
    if(!blog){
        return <i>blog is loading...</i>
    }
    return (
        <iframe className="pdf" 
                src=
                {blog}
            width="800" height="500">
        </iframe>
  )
}

export default ViewBlog
