import { IGroup, IUser } from "./interfaces";

export const getUsers = async () => {
  const users: IUser[] = await fetch(
    "https://browseeasyapi.azurewebsites.net/api/Users"
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
  const user: IUser[] = await fetch(
    `https://browseeasyapi.azurewebsites.net/api/Users`, {
        method: "POST",
        body: JSON.stringify(newUser),
        headers: {
        "content-type": "application/json",
        }
    }
  )
    .then((response) => response.json())
    .then((data) => data);
  return user;
};

export const putUsers = async (id: number, editedUser: Partial<IUser>) => {
  const user: IUser[] = await fetch(
    `https://browseeasyapi.azurewebsites.net/api/Users/${id}`, {
        method: "PUT",
        body: JSON.stringify(editedUser),
        headers: {
        "content-type": "application/json",
        }
    }
  )
    .then((response) => response.json())
    .then((data) => data);
  return user;
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