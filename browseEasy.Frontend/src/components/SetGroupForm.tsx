import { FC, useEffect, useState } from "react"
import "../stylesheets/SetGroupForm.css"
import { IGroup } from "../services/interfaces"
import { getGroups } from "../services/api"

type SetGroupFormProps = {
    openForm: () => void
}

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
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
        </form>
        <button onClick={homePage}>Go to preferences</button>
        </>
    )
}