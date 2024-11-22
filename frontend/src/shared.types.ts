export type User = {
    "id": number,
    "login": string,
    "email": string,
    "password": string
}

export type AccountResponse = {
    "success": boolean,
    "errors"?: string[],
    "login"?: string,
    "accessToken"?: string
}
