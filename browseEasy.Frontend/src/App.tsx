import { useEffect, useState } from 'react';
import './App.css'
import { getUsers } from './services/api';
import { IUser } from './services/interfaces';

function App() {
const [users, setUsers] = useState<IUser[]>();

  const getallUsers = async() => {
    const users = await getUsers();
    setUsers(users);
  }

  useEffect(() => {
    getallUsers();
  },[])

  return (
   <>
    {users != undefined && <p>{users[0].name}</p>}
   </>
  )
}

export default App
