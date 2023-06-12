import {makeAutoObservable} from "mobx";


export default class UnitPage {
  constructor() {
    makeAutoObservable(this)
  }

}