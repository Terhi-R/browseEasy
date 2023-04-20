import { FC } from "react"

type SetGroupFormProps = {
    openForm: () => void
}

export const SetGroupForm: FC<SetGroupFormProps> = ({openForm}) => {
    const homePage = () => {
        openForm();
    }

    return (
        <>
        <p>Group form is open</p>
        <button onClick={homePage}>Click me!</button>
        </>
    )
}