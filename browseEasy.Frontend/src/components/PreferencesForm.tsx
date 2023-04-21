import { FC } from "react"

type PreferenceFormProps = {
    openForm: () => void
}

export const PreferenceForm: FC<PreferenceFormProps> = ({openForm}) => {
    const homePage = () => {
        openForm();
    }

    return (
        <>
        <p>Preferences are open</p>
        <button onClick={homePage}>Let's go!</button>
        </>
    )
}