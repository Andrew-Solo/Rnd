import {makeAutoObservable} from "mobx";
import Game from "../models/Game";
import User from "../models/User";

export default class Session {
  constructor() {
    this.user = null;
    this.game = null;
    makeAutoObservable(this)
  }

  login(query: {login: string, password: string}) {
    
  }

  register() {

  }

  user: User | null
  game: Game | null

  get logged(): boolean {
    return this.user !== null;
  }
}