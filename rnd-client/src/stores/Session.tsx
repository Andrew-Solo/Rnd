import {makeAutoObservable} from "mobx";
import Game from "../models/Game";
import User, {UserRole} from "../models/User";

const user = new User({
  name: "AndrewSolo",
  email: "",
  image: "https://cdn.discordapp.com/attachments/1104404469090881556/1114280466275635361/486b66aea66db656.png"
});

const game = new Game({
  name: "mrak",
  owner: user,
  title: "Мрак",
  image: "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"
});

export default class Session {
  constructor() {
    this.user = user;
    this.game = game;
    makeAutoObservable(this)
  }

  user: User | null
  game: Game | null

  get logged(): boolean {
    return this.user !== null;
  }

  get role(): UserRole | null {
    return this.user?.role ?? null;
  }
}