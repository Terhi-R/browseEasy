import { FC, useContext, useEffect, useState } from "react"
import "../stylesheets/SetGroupForm.css"
import { IGroup } from "../services/interfaces"
import { getGroups } from "../services/api"
import firebase from 'firebase/compat/app';
import { UserContext } from "../App";

type SetGroupFormProps = {
    openForm: () => void
}

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
    const [googleUser, setUser] = useState<any>(null);
    const users = useContext(UserContext);
    const [groups, setGroups] = useState<IGroup[]>([]);
    const [foundGroup, setFoundGroup] = useState<boolean>(false);
    
    const homePage = () => {
        openForm();
    }

    const getallGroups = async() => {
        const groups = await getGroups();
        setGroups(groups);
    }
    
    const handleChange = (event: any) => {
        const found = {value: event.target.value};
        groups.map(group => {if (group.name == found.value) {
            setFoundGroup(true);
        } else {
            setFoundGroup(false);
        }})
    }

    const handleSubmit = () => {

    }

    useEffect(() => {
        getallGroups();
        firebase.auth().onAuthStateChanged(googleUser => {
        setUser(googleUser);
    })
    },[])

    return (
    <>
       {googleUser !== null && 
        <>
        <h2>Hi {googleUser.displayName}! Let's get you set up.</h2>
        <form className="group-form" onSubmit={handleSubmit}>
            <label>Your name</label>
            <input placeholder="[Name]"></input>
            <label>Your group name:</label>
            <input onChange={handleChange}></input>
            {foundGroup && 
            <>
            <label>We already found one! Do you have the unique key?</label>
            <input></input>
            </>
            }
            {!foundGroup && 
            <>
            <label>Create unique group keyword:</label>
            <input></input>
            </>
            }
        <button type="submit" onClick={homePage}>Go to preferences</button>
        </form>
        </>}
    </>
    )
}