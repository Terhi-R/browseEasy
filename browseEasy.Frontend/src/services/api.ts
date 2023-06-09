import { IGroup, IUser } from "./interfaces";

export const getUsers = async () => {
  const users: IUser[] = await fetch(
/*     "https://browseeasyapi.azurewebsites.net/api/Users" */
    "https://localhost:7233/api/Users"
  )
    .then((response) => response.json())
    .then((data) => data);
  return users;
};

export const getUser = async (id: number) => {
  const user: IUser[] = await fetch(
    `https://browseeasyapi.azurewebsites.net/api/Users/${id}`
  )
    .then((response) => response.json())
    .then((data) => data);
  return user;
};

export const postUsers = async (newUser: Partial<IUser>) => {
    console.log(newUser.loginId);
  const user: IUser[] = await fetch(
    `https://browseeasyapi.azurewebsites.net/api/Users`, {
        method: "POST",
        body: JSON.stringify(newUser),
        headers: {
        "content-type": "application/json",
        "access-control-allow-origin": "*",
        }
    }
  )
    .then((response) => response.json())
    .then((data) => data);
  return user;
};

export const putUsers = async (id: number, editedUser: Partial<IUser>) => {
  const request = {
    name: editedUser.name,
    loginId: editedUser.loginId,
    platforms: editedUser.platforms,
    type: editedUser.type,
    IMDbRating: editedUser.IMDbRating,
    genres: editedUser.genres, 
    groups: editedUser.groups,
    movies: editedUser.movies 
  }

  await fetch(
    `https://localhost:7233/api/Users/${id}`, {
/*     `https://browseeasyapi.azurewebsites.net/api/Users/${id}`, { */
        method: "PUT",
        body: JSON.stringify(request),
        headers: {
        "content-type": "application/json",
        },
    }
  )
};

export const deleteUsers = async (id: number) => {
  await fetch(
    `https://browseeasyapi.azurewebsites.net/api/Users/${id}`, {
        method: "DELETE"
    }
  )
};

export const getGroups = async () => {
  const groups: IGroup[] = await fetch(
    "https://browseeasyapi.azurewebsites.net/api/Groups"
  )
    .then((response) => response.json())
    .then((data) => data);
  return groups;
};