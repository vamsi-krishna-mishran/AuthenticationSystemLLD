import React, { useState } from 'react';
import { Button, Col, Drawer, Form, Input, InputNumber, Row, Select, Space } from 'antd';
import { EyeInvisibleOutlined, EyeTwoTone } from '@ant-design/icons';
import { Divider, message } from 'antd';
const { Option } = Select;
import { UserOutlined, } from '@ant-design/icons';
import URI from '../backendConfig.js'
import { useNavigate } from 'react-router-dom'

import { MyContext } from '../App.jsx';
import { useContext } from 'react';

const BaseURI=URI
const statesInIndia = [
  "Andhra Pradesh",
  "Arunachal Pradesh",
  "Assam",
  "Bihar",
  "Chhattisgarh",
  "Goa",
  "Gujarat",
  "Haryana",
  "Himachal Pradesh",
  "Jharkhand",
  "Karnataka",
  "Kerala",
  "Madhya Pradesh",
  "Maharashtra",
  "Manipur",
  "Meghalaya",
  "Mizoram",
  "Nagaland",
  "Odisha",
  "Punjab",
  "Rajasthan",
  "Sikkim",
  "Tamil Nadu",
  "Telangana",
  "Tripura",
  "Uttar Pradesh",
  "Uttarakhand",
  "West Bengal"
];

const countryList = ["India"]

function getCookie(name)
{
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return true;
  return false;
}

import './Login.css'
import { json } from 'react-router-dom';
function Login()
{
  // if (getCookie('token'))
  // {
  //   navigate('/');
  // }
  return (

    <div className="content">
      <h1 className="noto-sans-1">
        Welcome
      </h1>
      <h1 className="noto-sans-1">

        To Blogger
      </h1>

      <h4 className="noto-sans-2 p-2">
        Unlock the power of knowledge in just a few minutes
      </h4>
      {/* <button onClick={()=>openForm(1)} className="common-button sign-in noto-sans-1">sign-in</button>
        <button onClick={()=>openForm(2)} className="common-button sign-up noto-sans-1">register</button>
        {componentOpen} */}
      <LoginForm />
      <Register />
    </div>

  )
}

const Register = ({setCountt,setCount,user}) =>
{
  const [form] = Form.useForm();

  const apiCall = async (jsonObj) =>
  {
      let end=user?"api/User/update":"api/User/register";
    let call = await fetch(URI + end, {
      method: user?"PUT":"POST",
      body: jsonObj,
      "credentials": "include",

      headers: {
        "Accept": "*/*",
        "Content-Type": "application/json"

        // 'Content-Type': 'application/x-www-form-urlencoded',
      }
    });
    if (!call.ok)
    {
      throw new Error(await call.text())
    }
    let result = await call.text();
    return result
  }

  const [open, setOpen] = useState(user!=null);

  const onFinish = async (values) =>
  {

    try
    {
      let resultObj = {}
      // resultObj.id=0;
      resultObj.age = values.age;
      resultObj.name = values.name;
      resultObj.password = values.password;
      resultObj.userType = +values.userType;
      resultObj.userName = values.userName;

      let addressObj = {}
      // addressObj.id=0;
      addressObj.city = values.city;
      addressObj.country = values.country;
      addressObj.doorNo = values.doorNo;
      addressObj.street = values.street;
      addressObj.zipCode = String(values.zipCode)//.tostring();
      addressObj.state = values.state;

      resultObj.address = addressObj;
      resultObj = JSON.stringify(resultObj)
      console.log(resultObj)
      const result = await apiCall(resultObj)
      if (result)
      {
        message.success(user?"details updated successfully.":"user registered successfully.")
        form.resetFields();
        user&&setCount(false)
        user&&setCountt(prev=>prev+1)
      }
      else
      {
        message.error("user registration failed.")
      }
      console.log(resultObj)
    }
    catch (err)
    {
      message.error(err.message)
    }
  };


  const onFinishFailed = (errorInfo) =>
  {
    console.log('Failed:', errorInfo);
  };
  const showDrawer = () =>
  {
    setOpen(true);
  };
  const onClose = () =>
  {
    setOpen(false);
    if(user)setCount(false);
  };
  return (
    <>
      {/* <Button type="primary" onClick={showDrawer} icon={<PlusOutlined />}>
            New account
          </Button> */}
      {!user &&<button onClick={showDrawer} className="common-button sign-up noto-sans-1">register</button>}
      <Drawer
        title="Create a new Profile"
        width={720}
        onClose={onClose}
        open={open}
        styles={{
          body: {
            paddingBottom: 80,
          },
        }}
      // extra={
      //   <Space>
      //     <Button onClick={onClose} danger>Cancel</Button>
      //     <Button onClick={onClose} type="primary" danger>
      //       Submit
      //     </Button>
      //   </Space>
      // }
      >
        <Form initialValues={user&&{...user,reenterpassword:user.password,...user.address,userType:String(user.userType),gender:"male"}} form={form} layout="vertical" hideRequiredMark onFinish={onFinish} onFinishFailed={onFinishFailed}>
          <Row gutter={16}>
            <Col span={8}>
              <Form.Item
                name="name"
                label="Full Name"
                rules={[
                  {
                    required: true,
                    message: 'Please enter your full name',
                  },
                ]}
              >
                <Input placeholder="Please enter full name" />
              </Form.Item>
            </Col>

            <Col span={8}>
              <Form.Item
                name="userName"
                label="User Name"
                
                rules={[
                  {
                    required: true,
                    type: 'email',
                    message: 'Please enter email'
                    
                  },
                ]}
              >
                <Input disabled={user} placeholder="Please enter email id" />
              </Form.Item>
            </Col>
            <Col span={8}>
              <Form.Item
                name="userType"
                label="User Type"
                rules={[
                  {
                    required: true,
                    message: 'Please choose user type',
                  },
                ]}
              >
                <Select placeholder="Please choose user type">
                  <Option value="0">ADMIN</Option>
                  <Option value="1">STUDENT</Option>
                </Select>
              </Form.Item>
            </Col>

          </Row>


          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="age"
                label="age"
                rules={[
                  {
                    required: true,

                    message: 'Please enter age',
                  },
                ]}

              >
                <InputNumber style={{ width: "100%" }} placeholder="Please enter age" />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="gender"
                label="gender"
                
                rules={[
                  {
                    required: true,
                    message: 'Please choose your gender',
                  },
                ]}
              >
                <Select  placeholder="Please choose your gender">
                  <Option value="male">Male</Option>
                  <Option value="female">Female</Option>
                </Select>
              </Form.Item>
            </Col>
          </Row>





          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="password"
                label="Enter Password"
                rules={[
                  {
                    required: true,
                    message: 'Please enter password',
                  },
                ]}
              >
                <Input.Password placeholder="Please enter password" />
              </Form.Item>
            </Col>

            <Col span={12}>
              <Form.Item
                name="reenterpassword"
                label="Re-enter Password"
                dependencies={['password']}
                rules={[
                  {
                    required: true,

                    message: 'Please re enter password',
                  },
                  ({ getFieldValue }) => ({
                    validator(_, value)
                    {
                      if (!value || getFieldValue('password') === value)
                      {
                        return Promise.resolve();
                      }
                      return Promise.reject(new Error('The two passwords that you entered do not match!'));
                    },
                  }),
                ]}
              >
                <Input.Password placeholder="Please re-enter password" iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)} />
              </Form.Item>
            </Col>


          </Row>
          <Divider orientation="left">Address</Divider>


          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="doorNo"
                label="Door No"
                rules={[
                  {
                    required: true,

                    message: 'Please enter door no.',
                  },
                ]}

              >
                <Input placeholder="Please enter door no." />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="street"
                label="street"
                rules={[
                  {
                    required: true,
                    message: 'Please enter street',
                  },
                ]}
              >
                <Input placeholder="Please enter street." />
              </Form.Item>
            </Col>
          </Row>

          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="city"
                label="City"
                rules={[
                  {
                    required: true,

                    message: 'Please enter city.',
                  },
                ]}

              >
                <Input placeholder="Please enter city." />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="state"
                label="state"
                rules={[
                  {
                    required: true,
                    message: 'Please enter state',
                  },
                ]}
              >
                {/* <Input  placeholder="Please enter state." /> */}
                <Select placeholder="Please choose your state">
                  {statesInIndia.map((state, ix) => <Option value={state}>{state}</Option>)}
                </Select>
              </Form.Item>
            </Col>
          </Row>


          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="country"
                label="Country"
                rules={[
                  {
                    required: true,

                    message: 'Please enter country',
                  },
                ]}

              >
                {/* <Input  placeholder="Please enter country." /> */}
                <Select placeholder="Please entre country.">
                  {countryList.map((country, ix) => <Option value={country}>{country}</Option>)}
                </Select>
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="zipCode"
                label="Zipcode"
                rules={[
                  {
                    required: true,
                    message: 'Please enter zipcode.',
                  },
                ]}
              >
                <InputNumber style={{ width: "100%" }} placeholder="Please enter zipcod." />
              </Form.Item>
            </Col>
          </Row>

          <Row>
            <Space>
              <Button danger>Cancel</Button>
              <Button type="primary" danger htmlType='submit'>
                {user?"Update":"Submit"}
              </Button>
            </Space>
          </Row>

        </Form>
      </Drawer>
    </>
  );
}

const LoginForm = () =>
{
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [form] = Form.useForm();
  const { text, setText } = useContext(MyContext);

  const showDrawer = () =>
  {
    setOpen(true);
  };
  const onClose = () =>
  {
    setOpen(false);
  };
  const apiCall = async (uri) =>
  {
    let call = await fetch(BaseURI + "api/User/login?" + uri, {
      method: "POST",
      "credentials": "include",

    });
    if (!call.ok)
    {
      setText(false)
      throw new Error(call.statusText)
    }
    let result = await call.text();
    const isadmin=result;
    //console.log(isadmin)
    //console.log(typeof isadmin)
    setText(isadmin=='true')
    //setText(true)
    return result
  }

  const onFinish = async (values) =>
  {
    try
    {
      const uri = `uname=${values.name}&pwd=${values.password}`;
      console.log(uri)
      const res = await apiCall(uri);
      form.resetFields();
      
      message.success("user login success.")
      navigate('/')
    }
    catch (err)
    {
      message.error(err.message)
    }

  };
  const onFinishFailed = (errorInfo) =>
  {
    console.log('Failed:', errorInfo);
  };
  return (
    <>
      {/* <Button type="primary" onClick={showDrawer} icon={<PlusOutlined />}>
            New account
          </Button> */}
      <button onClick={showDrawer} className="common-button sign-in noto-sans-1">sign-in</button>
      <Drawer
        title="Log into your Account."
        width={520}
        onClose={onClose}
        open={open}
        styles={{
          body: {
            paddingBottom: 80,
          },
        }}
      // extra={
      //   <Space>
      //     <Button onClick={onClose} danger>Cancel</Button>
      //     <Button onClick={onClose} type="primary" danger htmlType="submit">
      //       Sign in
      //     </Button>
      //   </Space>
      // }
      >
        <Form form={form} layout="vertical" hideRequiredMark onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off">
          <Row gutter={16}>
            <Col span={24}>
              <Form.Item
                name="name"
                label="User Name"
                rules={[
                  {
                    required: true,
                    type: 'email',
                    message: 'Please enter user name',
                  },
                ]}
              >
                <Input addonBefore={<UserOutlined />} placeholder="Please enter user name" />
              </Form.Item>
            </Col>

          </Row>
          <Row gutter={16}>
            <Col span={24}>
              <Form.Item
                name="password"
                label="password"
                rules={[
                  {
                    required: true,

                    message: 'Please enter password',
                  },
                ]}
              >
                <Input.Password placeholder="Please enter password" />
              </Form.Item>
            </Col>
          </Row>
          <Row>
            <Space>
              <Button danger>Cancel</Button>
              <Button type="primary" danger htmlType="submit">
                Sign in
              </Button>
            </Space>
          </Row>

        </Form>
      </Drawer>
    </>
  );
}
export default Login
export {Register}