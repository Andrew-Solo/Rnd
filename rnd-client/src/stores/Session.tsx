import {makeAutoObservable} from "mobx";
import User from "./models/User";

export default class Session {
  constructor() {
    this.user = new User({
      id: "2def7f2e-feca-4871-a424-ed32f9025766",
      name: "andrewsolo",
      path: "users/andrewsolo",
      title: "AndrewSolo",
      image: "https://cdn.discordapp.com/attachments/1104404469090881556/1114280466275635361/486b66aea66db656.png"
    });
    makeAutoObservable(this)
  }

  user: User | null

  get logged(): boolean {
    return this.user !== null;
  }
}