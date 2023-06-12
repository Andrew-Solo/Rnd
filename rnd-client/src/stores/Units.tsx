import {makeAutoObservable} from "mobx";
import Store from "./Store";
import Unit from "../models/Unit";


export default class Units {
  constructor(store: Store) {
    this.store = store;
    this.data = [];
    makeAutoObservable(this, {
      store: false,
    })
  }

  readonly store: Store
  data: Unit[]
}