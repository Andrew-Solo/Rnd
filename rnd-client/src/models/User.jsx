export default class User {
  constructor(user) {
    this.id = user.id;
    this.email = user.email;
    this.login = user.login;
    this.role = user.role;
    this.image = user.image;
    this.discordId = user.discordId;
    this.registered = user.registered;
    this.games = user.games;
  }

  id
  email
  login
  role
  image
  discordId
  registered: Date
  games
}

export const UserRoles = {
  Viewer: "Viewer",
  Editor: "Editor",
  Admin: "Admin",
}