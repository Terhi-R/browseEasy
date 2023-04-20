import { FC } from "react"
import { Matches } from "./Matches"
import { PickMovies } from "./PickMovies"

type PickingGalleryProps = {
    openForm: () => void
}

export const PickingGallery: FC<PickingGalleryProps> = ({openForm}) => {
    const homePage = () => {
        openForm();
    }

    return (
        <>
        <p>Picking Gallery is open</p>
        <button onClick={homePage}>Click me!</button>
        <PickMovies/>
        <Matches/>
        </>
    )
}