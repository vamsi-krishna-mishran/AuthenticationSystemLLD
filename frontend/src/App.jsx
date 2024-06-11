import React from 'react';
import "./App.css"
import Layout from './components/LayoutC';
import { message } from 'antd';

import Login from './components/Login';
import BaseURI from './backendConfig'
import ViewBlog from './components/ViewBlog'
import
{
  BrowserRouter as Router,
  NavLink,
  Routes,
  Route,
} from "react-router-dom";
import { createContext } from 'react';
import { useState } from 'react';
const MyContext = createContext(true);






const App = () =>
{
  const [text, setText] = useState("");
  return (
    <MyContext.Provider value={{text,setText}}>
    

  
    <>


      <Router>
        <div className="navbar">
          <NavLink to="/">

            <img className="nav-icon" src="https://i.pinimg.com/originals/16/1c/ff/161cff19e668e270ccb1b98856ebd81e.png" />
          </NavLink>
        </div>
        <Routes>

          <Route
            path="/login"
            element={<Login />}
          />
          <Route path="/blogs/:blogId" element={<ViewBlog />} />


          <Route path="/" element={<Layout />} >

          </Route>
        </Routes>
      </Router>

    </>
    </MyContext.Provider>)

};
export default App;

export {MyContext}