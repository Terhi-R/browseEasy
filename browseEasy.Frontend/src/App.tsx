import { createContext, useEffect, useState } from 'react';
import './App.css'
import { getUsers } from './services/api';
import { IUser } from './services/interfaces';
import { Navbar } from './components/Navbar';
import { PageController } from './components/PageController';

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
    <PageController/>
    <p>{users[0].name}</p>
   </userContext.Provider>
  )
}

export default App
