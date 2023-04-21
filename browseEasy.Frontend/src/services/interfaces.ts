export interface IUser {
    id: number,
    name: string,
    loginId: string,
    platforms: IPlatform[],
    type: string,
    IMDbRating: number,
    genres: IGenre[],
    groups: IGroup[],
    movies: IMovie[]
}

export interface IPlatform {
    id: number,
    name: string
}

export interface IGenre {
    id: number,
    name: string
}

export interface IMovie {
    id: number,
    name: string
}

export interface IGroup {
    id: number,
    name: string | undefined,
    uniqueKey: string | undefined
}

export interface IActiveUser {
    id: string,
    name: string
}