import { FC } from "react"
import { Matches } from "./Matches"
import { PickMovies } from "./PickMovies"

export const PickingGallery = () => {

    return (
        <>
        <p>Picking Gallery is open</p>
        <PickMovies/>
        <Matches/>
        </>
    )
}