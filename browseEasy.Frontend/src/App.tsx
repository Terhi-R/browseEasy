import { createContext, useEffect, useState } from 'react';
import './App.css'
import { getUsers } from './services/api';
import { IUser } from './services/interfaces';
import { Navbar } from './components/Navbar';
import { PageController } from './components/PageController';
import { Navigate, Route, Routes } from 'react-router-dom';
import { Settings } from './components/Settings';
import { About } from './components/About';

function App() {
const [users, setUsers] = useState<IUser[]>([]);
const userContext = createContext<IUser[]>([]);

  const getallUsers = async() => {
    const users = await getUsers();
    setUsers(users);
  }
  
  useEffect(() => {
    getallUsers();
  },[])

  return (
    <userContext.Provider value={users}>
        <Navbar/>
        <Routes location={location} key={location.pathname}>
          <Route
            path="/"
            element={<PageController />}
          ></Route>
          <Route 
            path="/settings" 
            element={<Settings />}
          ></Route>
          <Route
            path="/about"
            element={<About />}
          ></Route>
          <Route 
            path="*"
            element={<Navigate to="/" replace={true} />}
          ></Route>
        </Routes>
    </userContext.Provider>
  )
}

export default App
