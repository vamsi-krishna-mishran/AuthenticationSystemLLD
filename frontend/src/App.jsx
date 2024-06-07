import React from 'react';
import "./App.css"
import Layout from './components/LayoutC';

import Login from './components/Login';
import
{
  BrowserRouter as Router,
  Link,
  Routes,
  Route,
} from "react-router-dom";







const App = () =>
{
  return (
    <>

      <div className="navbar">
        <img className="nav-icon" src="https://i.pinimg.com/originals/16/1c/ff/161cff19e668e270ccb1b98856ebd81e.png" />
      </div>
      <Router>
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