import { FC, useEffect, useState } from "react"
import "../stylesheets/SetGroupForm.css"
import { IGroup } from "../services/interfaces"
import { getGroups } from "../services/api"

type SetGroupFormProps = {
    openForm: () => void
}

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
    const [groups, setGroups] = useState<IGroup[]>([]);
    
    const homePage = () => {
        openForm();
    }

    const getallGroups = async() => {
        const groups = await getGroups();
        setGroups(groups);
    }

    useEffect(() => {
        getallGroups();
    },[])

    return (
        <>
        <h2>Hi [Name]! Let's get you set up.</h2>
        <form className="group-form">
            <label>Your name</label>
            <input placeholder="[Name]"></input>
            <label>Your group name:</label>
            <input></input>
            <label>We already found one! Do you have the unique key?</label>
            <input></input>
            <label>Create unique group keyword:</label>
            <input></input>
        </form>
        <button onClick={homePage}>Go to preferences</button>
        </>
    )
}