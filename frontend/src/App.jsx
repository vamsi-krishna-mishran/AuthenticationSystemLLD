import React from 'react';
import "./App.css"
import Layout from './components/LayoutC';
import { message } from 'antd';

import Login from './components/Login';
import BaseURI from './backendConfig'
import
{
  BrowserRouter as Router,
  NavLink,
  Routes,
  Route,
} from "react-router-dom";






const App = () =>
{
  return (
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



          <Route path="/" element={<Layout />} >

          </Route>
        </Routes>
      </Router>

    </>)

};
export default App;