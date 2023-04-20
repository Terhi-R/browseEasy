import { Header } from "./Header"
import { PickingGallery } from "./PickingGallery"
import { PreferenceForm } from "./PreferencesForm"
import { SetGroupForm } from "./SetGroupForm"

export const PageController = () => {
    return (
        <>
        <Header/>
        <PreferenceForm/>
        <SetGroupForm/>
        <PickingGallery/>
        </>
    )
}