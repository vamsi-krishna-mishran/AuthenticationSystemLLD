import React from 'react'
import { useState, useEffect } from 'react'
import BaseURI from '../backendConfig'
import { Divider, message } from 'antd';

function Profile()
{
    const [profile, setProfile] = useState(null);
    const apiCall = async () =>
    {
        let call = await fetch(BaseURI + "api/User/resource1", {
            "credentials": "include"

        })
        if (!call.ok)
        {
            throw new Error(await call.text());
        }
        return await call.text();
    }
    useEffect(() =>
    {

        (async function ()
        {
            try
            {
                let res = await apiCall();
                //message.success(res)
                setProfile(res)
            }
            catch (err)
            {
                message.error(err.message)
            }
        })()
    }, [])

    return (
        <div>{profile ? profile : "loading user"}</div>
    )
}

export default Profile