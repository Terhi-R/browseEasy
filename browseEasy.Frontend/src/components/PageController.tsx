import { FC, useEffect, useState } from "react"
import { Header } from "./Header"
import { PickingGallery } from "./PickingGallery"
import { PreferenceForm } from "./PreferencesForm"
import { SetGroupForm } from "./SetGroupForm"


export const PageController = () => {
    const [home, setHome] = useState<Boolean>(true);
    const [openPreferenceForm, setPreferenceForm] = useState<Boolean>(false);
    const [openPickingGallery, setPickingGallery] = useState<Boolean>(false);

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

    return (
        <>
        <button onClick={() => setHome(true)}>Home</button>
        {home && <Header openForm={groupForm}/>}
        {!home && !openPreferenceForm && !openPickingGallery && <SetGroupForm openForm={preferenceForm}/>}
        {!home && openPreferenceForm && !openPickingGallery && <PreferenceForm openForm={pickingGallery}/>}
        {!home && !openPreferenceForm && openPickingGallery && <PickingGallery openForm={() => {groupForm(); setHome(true);}}/>}
        </>
    )
}