import { FC, SyntheticEvent, useContext, useEffect, useState } from "react"
import "../stylesheets/SetGroupForm.css"
import firebase from 'firebase/compat/app';
import { GroupContext, UserContext } from "../App";
import { putUsers } from "../services/api";
import { IGroup, IUser } from "../services/interfaces";
import { ActiveUserContext } from "./PageController";

type SetGroupFormProps = {
    openForm: () => void
}

const initialValues = {
  userName: "",
  groupName: "",
  newUniqueKey: "",
  foundGroupKey: "",
  uniqueKey: "",
  foundGroup: false,
};

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
    const users = useContext(UserContext);
    const groups = useContext(GroupContext);
    const activeUser = useContext(ActiveUserContext);
    const [values, setValues] = useState(initialValues);
    
    const preferencesPage = () => {
        openForm();
    }

    const handleChange = (e: SyntheticEvent) => {
        const target = e.target as typeof e.target & {
        userName: { value: string };
        groupName: { value: string };
        newUniqueKey: { value: string | undefined };
        uniqueKey: { value: string | undefined };
        };

        groups.map(group => {
            if (group.name == target.groupName.value) {
            setValues({...values, foundGroup: true, foundGroupKey: group.uniqueKey!})
        } else {
            setValues({...values, foundGroup: false})
        }});
        console.log(target.groupName.value);
        setValues({
        ...values,
        userName: target.userName.value,
        groupName: target.groupName.value,
        newUniqueKey: target.newUniqueKey.value === undefined ? "" : target.newUniqueKey.value,
        uniqueKey: target.uniqueKey.value === undefined ? "" : target.uniqueKey.value,
        });
    }

    const handleSubmit = (e: SyntheticEvent) => {
        e.preventDefault();
        if (initialValues.foundGroup) {
            console.log("found Group");
            groups.map(group => {
                if (group.uniqueKey == initialValues.uniqueKey && initialValues.uniqueKey == initialValues.foundGroupKey) {
                    users.map(user => {
                        if (user.loginId === activeUser.id) {
                            user.groups.push(group);
                            console.log(user.groups);
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
                    name: initialValues.groupName,
                    uniqueKey: initialValues.newUniqueKey
                }
                user.groups.push(newGroup);
                console.log(user.groups);
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
            <input type="text" name="userName" onChange={handleChange} placeholder={activeUser.name}></input>
            <label>Your group name:</label>
            <input type="text" name="groupName" onChange={handleChange}></input>
            {initialValues.foundGroup && 
            <>
            <label>We already found one! Do you have the unique key?</label>
            <input type="text" name="uniqueKey" onChange={handleChange}></input>
            </>
            }
            {!initialValues.foundGroup && 
            <>
            <label>Create unique group keyword:</label>
            <input type="text" name="newUniqueKey" onChange={handleChange}></input>
            </>
            }
            <button type="submit">Go to preferences</button>
        </form>
        </>}
    </>
    )
}