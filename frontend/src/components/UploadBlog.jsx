import React,{useState} from 'react';
import { Button, Form, Input, Space } from 'antd';
import {UploadOutlined} from '@ant-design/icons'
import './UploadBlog.css'
import ThumbnailFile from './ThumbnailFile';
import { message } from 'antd';
import API from '../API';
import {getBase64} from '../API'


const SubmitButton = ({ form, children }) => {
  const [submittable, setSubmittable] = React.useState(false);

  // Watch all values
  const values = Form.useWatch([], form);
  React.useEffect(() => {
    form
      .validateFields({
        validateOnly: true,
      })
      .then(() => setSubmittable(true))
      .catch(() => setSubmittable(true));
  }, [form, values]);
  return (
    <Button id="uploadBtn" icon={<UploadOutlined/>} style={{backgroundColor:'rgb(223,32,59)',padding:'2rem',fontWeight:'bold',color:'white',fontSize:'1.5rem'}} type="primary" htmlType="submit" disabled={!submittable}>
      {children}
    </Button>
  );
};
const UploadBlog = () => {
  const [form] = Form.useForm();
  
  const [img,setImg]=useState("")
  const ssetImg=(file)=>{
    console.log(file)
    setImg(file)
  }
  const [blog,setBlog]=useState("")

  const ssetBlog=(file)=>{
    console.log(file)
    setBlog(file)
  }

  const uploadForm=async (values)=>{
    // alert('triggered')
    // console.log(values.name)
    // console.log(img)
    // console.log(blog)
    try{
        if(!values.name||values.name.trim()==""){
            message.error("title is required to publish.")
            return;
        }
    
        if(!img){
            message.error("thumbnail image is required to publish.")
            return;
        }
        if(!blog){
            message.error("blog document is required to publish.")
            return;
        }
        let obj={}
        obj.blogHeading=values.name.trim()
        obj.blogThumbnailImg=await getBase64(img.originFileObj)
        obj.blogDocument=await getBase64(blog.originFileObj)
        console.log(obj)
        let call = await API("api/Blogs/addblog", {
            headers: {
                'Accept': '*/*',
                'Content-Type': 'application/json'
              },
            "method":"POST",
            "credentials": "include",
            "body":JSON.stringify(obj)
        });
        
        if(!call.ok){
            throw new Error(await call.text());
        }
        



        message.success("blog published successfully.")
        form.resetFields();
        console.log(values);
    }
    catch(err){
        //message.error("error")
        message.error(err.message)
    }
  } 

  const showError=(values)=>{
    console.log(values)
  }
  return (
    <Form onFinish={uploadForm} onFinishFailed={showError} form={form} name="validateOnly" layout="vertical" autoComplete="off">
      <Form.Item
        style={{fontSize:"5rem"}}
        name="name"
        label="Title"
        rules={[
          {
            required: false,
          },
        ]}
      >
        <Input />
      </Form.Item>
      <Form.Item
        style={{fontSize:"5rem"}}
        name="blogThumbnail"
        label="Thumbnail Image"
        rules={[
          {
            required: false,
          },
        ]}
      >
        <ThumbnailFile name="blogThumbnail" setFile={ssetImg}/>
      </Form.Item>

      <Form.Item
        style={{fontSize:"5rem"}}
        name="docblogThumbnail"
        label="Upload Document"
        rules={[
          {
            required: false,
          },
        ]}
      >
        <ThumbnailFile name="docblogThumbnail" setFile={ssetBlog}/>
      </Form.Item>
      {/* <Form.Item
        name="age"
        label="Age"
        rules={[
          {
            required: true,
          },
        ]}
      >
        <Input />
      </Form.Item> */}
      <Form.Item>
        <Space>
          <SubmitButton form={form}>Publish Blog</SubmitButton>
        </Space>
      </Form.Item>
    </Form>
  );
};
export default UploadBlog;