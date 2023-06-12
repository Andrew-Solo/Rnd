import {makeAutoObservable} from "mobx";
import Store from "./Store";
import Module from "../models/Module";
import {client} from "../data/Client";


export default class Modules {
  constructor(store: Store) {
    this.store = store;
    this.userId = store.session.user?.id ?? "";
    this.data = [];

    this.syncModules();

    makeAutoObservable(this, {
      store: false,
      userId: false
    })
  }

  syncModules() {
    const {data} = client.modules.list();
    if (data !== null) this.data = data;
  }

  readonly store: Store
  userId: string
  data: Module[]
}