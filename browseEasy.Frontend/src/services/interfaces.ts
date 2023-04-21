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
    name: string
}

export interface IGenre {
    name: string
}

export interface IMovie {
    name: string
}

export interface IGroup {
    name: string | undefined,
    uniqueKey: string | undefined
}

export interface IActiveUser {
    id: string,
    name: string
}