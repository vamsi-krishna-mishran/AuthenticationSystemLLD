import React from 'react'
import { useState, useEffect } from 'react'
import BaseURI from '../backendConfig'
import { Divider, message } from 'antd';
import BlogStat from './BlogStat';
import './Profile.css'
import { Watermark } from "antd";
import API from '../API';
import BlogProfile from './BlogProfile';

function Profile()
{
    const [profile, setProfile] = useState(null);
    const [countt, setCountt] = useState(0);
    // const apiCall = async () =>
    // {
    //     let call = await fetch(BaseURI + "api/User/getDetails", {
    //         "credentials": "include",
    //         headers: {
    //             'Accept': 'application/json',
    //             'Content-Type': 'application/json'
    //           },


    //     })
    //     if (!call.ok)
    //     {
    //         throw new Error(await call.text());
    //     }
    //     return await call.text();
    // }
    useEffect(() =>
    {

        (async function ()
        {
            try
            {
                let apicall = await API('api/User/getDetails', {
                    "credentials": "include",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
                if (!apicall.ok)
                {
                    throw new Error(await apicall.text());
                }
                const userdetails = await apicall.json();
                console.log(userdetails)
                setProfile(userdetails)
                //message.success(res)
                //setProfile(res)
                //console.log('hi')
            }
            catch (err)
            {
                message.error(err.message)
            }
        })()
    }, [countt])

    return (
        <Watermark content={profile?.name ?? ""}>
            <div className='blog-profile'>
                {profile && <BlogProfile setCountt={setCountt} user={profile} />}
                {/* <div>{profile ? profile : "loading user"}</div> */}
                <BlogStat />
            </div>
        </Watermark>

    )
}

export default Profile