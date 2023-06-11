import {makeAutoObservable} from "mobx";
import User, {UserRole} from "models/User";

const user = new User({
  login: "AndrewSolo",
  image: "https://cdn.discordapp.com/attachments/1104404469090881556/1114280466275635361/486b66aea66db656.png"
});

export default class Session {
  constructor() {
    this.user = user;
    makeAutoObservable(this)
  }

  user: User | null

  get logged(): boolean {
    return this.user !== null;
  }

  get role(): UserRole | null {
    return this.user?.role ?? null;
  }
}