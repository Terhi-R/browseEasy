import { FC, createContext, useEffect, useState } from "react"
import { Header } from "./Header"
import { PickingGallery } from "./PickingGallery"
import { PreferenceForm } from "./PreferencesForm"
import { SetGroupForm } from "./SetGroupForm"
import { IActiveUser } from "../services/interfaces"
import { UserContext } from "../App"

export const ActiveUserContext = createContext<IActiveUser>({id: "", name: ""});

export const PageController = () => {
    const [home, setHome] = useState<Boolean>(true);
    const [openPreferenceForm, setPreferenceForm] = useState<Boolean>(false);
    const [openPickingGallery, setPickingGallery] = useState<Boolean>(false);
    const [active, setActiveUser] = useState<IActiveUser>({id: "", name: ""});

    const groupForm = () => {
        setHome(false);
        setPreferenceForm(false);
        setPickingGallery(false);
    }

    const preferenceForm = () => {
        setHome(false);
        setPreferenceForm(true);
        setPickingGallery(false);
    }

    const pickingGallery = () => {
        setHome(false);
        setPreferenceForm(false);
        setPickingGallery(true);
    }

    const activeUser = (user: IActiveUser) => {
        setActiveUser(user);
    }

    return (
    <ActiveUserContext.Provider value={active}>
        <button onClick={() => setHome(true)}>Home</button>
        {home && <Header openForm={groupForm} activeUser={activeUser}/>}
        {!home && !openPreferenceForm && !openPickingGallery && <SetGroupForm openForm={preferenceForm}/>}
        {!home && openPreferenceForm && !openPickingGallery && <PreferenceForm openForm={pickingGallery}/>}
        {!home && !openPreferenceForm && openPickingGallery && <PickingGallery />}
    </ActiveUserContext.Provider>
    )
}