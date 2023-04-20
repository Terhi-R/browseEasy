import { FC } from "react";

type HeaderProps = {
    openForm: () => void
}

export const Header: FC<HeaderProps> = ({openForm}) => {
    const login = () => {
        //add here Google Login
    openForm();
    }

    return (
        <>
        <p>Home!</p>
        <button onClick={login}>Get started</button>
        </>
    )
}