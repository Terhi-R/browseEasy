import { FC, useContext, useEffect, useState } from "react"
import "../stylesheets/SetGroupForm.css"
import firebase from 'firebase/compat/app';
import { GroupContext, UserContext } from "../App";
import { putUsers } from "../services/api";
import { IUser } from "../services/interfaces";

type SetGroupFormProps = {
    openForm: () => void
}

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
    const [googleUser, setUser] = useState<any>(null);
    const users = useContext(UserContext);
    const groups = useContext(GroupContext);
    const [foundGroup, setFoundGroup] = useState<boolean>(false);
    const [foundGroupKey, setFoundGroupKey] = useState<string>();
    const [userName, setUserName] = useState<string>();
    const [newUniqueKey, setNewUniqueKey] = useState<string>();
    const [uniqueKey, setUniqueKey] = useState<string>();
    
    const homePage = () => {
        openForm();
    }

    const handleChange = (event: any) => {
        const current = {value: event.target.value};
        groups.map(group => {
            if (group.name == current.value) {
            setFoundGroup(true);
            setFoundGroupKey(group.uniqueKey);
        } else {
            setFoundGroup(false);
        }})
    }

    const handleSubmit = () => {
        if (foundGroup) {
            groups.map(group => {
                if (group.uniqueKey == uniqueKey && uniqueKey == foundGroupKey) {
               /*      await putUsers() */
                }})
        }
    }

    const putUsers = async (id: number, user: IUser) => {
        await putUsers(id, user);
    }
/*       const getallUsers = async() => {
    const users = await getUsers();
    setUsers(users);
  } */
    useEffect(() => {
        firebase.auth().onAuthStateChanged(googleUser => {
        setUser(googleUser);
    })
    },[])

    return (
    <>
       {googleUser !== null && 
        <>
        <h2>Hi {googleUser.displayName.split(' ')[0]}! Let's get you set up.</h2>
        <form className="group-form" onSubmit={handleSubmit}>
            <label>Your name</label>
            <input placeholder="[Name]" type="text" onChange={(e) => {setUserName(e.target.value)}}></input>
            <label>Your group name:</label>
            <input onChange={handleChange}></input>
            {foundGroup && 
            <>
            <label>We already found one! Do you have the unique key?</label>
            <input type="text" onChange={(e) => {setUniqueKey(e.target.value)}}></input>
            </>
            }
            {!foundGroup && 
            <>
            <label>Create unique group keyword:</label>
            <input type="text" onChange={(e) => {setNewUniqueKey(e.target.value)}}></input>
            </>
            }
        <button type="submit" onClick={homePage}>Go to preferences</button>
        </form>
        </>}
    </>
    )
}