import {makeAutoObservable} from "mobx";
import Provider from "../data/Provider";
import Result from "../data/Result";
import Message from "../data/Message";

export default class Collection<T> {
  load(): void {
    this.loaded = false;
    this.failed = false;
    this.message = null;
    this.data = [];

    this.provider.list().then(this.loadSuccess);
  }

  loadSuccess(result: Result<T[]>): void {
    this.loaded = true;
    this.failed = !result.success;
    this.message = result.message;
    this.data = result.data;
  }

  loaded: boolean
  failed: boolean
  message: Message | null
  data: T[]

  provider : Provider<T>

  constructor(provider : Provider<T>) {
    this.loaded = false;
    this.failed = false;
    this.message = null;
    this.data = [];
    this.provider = provider;

    makeAutoObservable(this, {
      provider: false
    }, { autoBind: true })
  }
}