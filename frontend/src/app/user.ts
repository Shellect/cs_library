export type User = {
    "Id": Number,
    "Login": String,
    "Email": String,
    "Password": String
}

export type UsersResponse = {
    "success": boolean,
    "result": User[],
    "total": Number
}
