import { FC } from "react";
import { signInWithGoogle } from '../services/firebase';

type HeaderProps = {
    openForm: () => void
}

export const Header: FC<HeaderProps> = ({openForm}) => {
    const login = () => {
    signInWithGoogle();
    openForm();
    }

    return (
        <>
        <p>Home!</p>
        <button onClick={login}>Get started</button>
        </>
    )
}