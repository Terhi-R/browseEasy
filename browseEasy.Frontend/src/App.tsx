import { createContext, useEffect, useState } from 'react';
import './App.css'
import { getGroups, getUsers } from './services/api';
import { IGroup, IUser } from './services/interfaces';
import { Navbar } from './components/Navbar';
import { PageController } from './components/PageController';
import { Navigate, Route, Routes } from 'react-router-dom';
import { Settings } from './components/Settings';
import { About } from './components/About';

export const UserContext = createContext<IUser[]>([]);
export const GroupContext = createContext<IGroup[]>([]);

function App() {
  const [users, setUsers] = useState<IUser[]>([]);
  const [groups, setGroups] = useState<IGroup[]>([]);


  const getallUsers = async() => {
    const users = await getUsers();
    setUsers(users);
  }

  const getallGroups = async() => {
    const groups = await getGroups();
    setGroups(groups);
  }

  useEffect(() => {
    getallUsers();
    getallGroups();
  },[])

  return (
    <UserContext.Provider value={users}>
    <GroupContext.Provider value={groups}>
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
    </GroupContext.Provider>
    </UserContext.Provider>
  )
}

export default App
