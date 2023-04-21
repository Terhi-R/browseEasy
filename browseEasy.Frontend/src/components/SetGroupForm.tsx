import { ChangeEvent, FC, FormEvent, SyntheticEvent, useContext, useEffect, useRef, useState } from "react"
import "../stylesheets/SetGroupForm.css"
import firebase from 'firebase/compat/app';
import { GroupContext, UserContext } from "../App";
import { putUsers } from "../services/api";
import { IGroup, IUser } from "../services/interfaces";
import { ActiveUserContext } from "./PageController";

type SetGroupFormProps = {
    openForm: () => void
}

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
    const users = useContext(UserContext);
    const groups = useContext(GroupContext);
    const activeUser = useContext(ActiveUserContext);
    const [matchingGroup, setMatchingGroup] = useState(false);
    
    const preferencesPage = () => {
        openForm();
    }

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const groupName = e.currentTarget.value;
         if (groupName !== undefined && groupName.length > 0) {
            groups.map(group => {
                if (group.name == groupName) {
                setMatchingGroup(true);
                } else {
                    setMatchingGroup(false);
                }});
        } 
    }

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const target = e.currentTarget.elements as typeof e.currentTarget.elements & {
        userName: { value: string };
        groupName: { value: string };
        uniqueKey: { value: string };
        };

        if (matchingGroup) {
            console.log("found Group");
            groups.map(group => {
                if (group.uniqueKey == target.uniqueKey.value) {
                    users.map(user => {
                        if (user.loginId === activeUser.id) {
                            user.name = target.userName.value;
                            user.groups.push(group);
                            addGroupToUser(user.id, user);
                        }
                    })
                }})
                preferencesPage();
                return;
            }
            users.map(user => {
                if (user.loginId === activeUser.id) {
                const newGroup : IGroup = {
                    name: target.groupName.value,
                    uniqueKey: target.uniqueKey.value
                }
                user.name = target.userName.value;
                user.groups.push(newGroup);
                 console.log(user.groups);
                console.log(newGroup); 
                addGroupToUser(user.id, user);
            }
        })
        preferencesPage();
    }

    const addGroupToUser = async (id: number, user: IUser) => {
        await putUsers(id, user);
    }

    return (
    <>
       {activeUser.id !== null && 
        <>
        <h2>Hi {activeUser.name}! Let's get you set up.</h2>
        <form className="group-form" onSubmit={handleSubmit}>
            <label>Your name</label>
            <input type="text" name="userName" defaultValue={activeUser.name}></input>
            <label>Your group name:</label>
            <input type="text" name="groupName" onChange={handleChange}></input>
            {matchingGroup && 
            <>
            <label>We already found one! Do you have the unique key?</label>
            <input type="text" name="uniqueKey"></input>
            </>
            }
            {!matchingGroup && 
            <>
            <label>Create unique group keyword:</label>
            <input type="text" name="uniqueKey"></input>
            </>
            }
            <button type="submit">Go to preferences</button>
        </form>
        </>}
    </>
    )
}