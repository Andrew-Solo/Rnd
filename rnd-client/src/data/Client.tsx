﻿import MockProvider from "./MockProvider";
import Module from "../models/Module";
const { v4: uuid } = require('uuid');

const modulesMock: Module[] = [
  new Module({
    id: uuid(),
    name: "games",
    path: "games",
    title: "Игры",
    icon: "<svg xmlns=\"http://www.w3.org/2000/svg\" height=\"24\" viewBox=\"0 -960 960 960\" width=\"24\"><path d=\"M225.385-185.385h155.77v-245h197.69v245h155.77v-381.922L480-759.23 225.385-567.436v382.051Zm-45.384 45.384v-449.998L480-815.767l299.999 225.768v449.998H533.462v-245H426.538v245H180.001ZM480-472.615Z\"/></svg>",
  }),
  new Module({
    id: uuid(),
    name: "characters",
    path: "characters",
    title: "Персонажи",
    icon: "<svg xmlns=\"http://www.w3.org/2000/svg\" height=\"24\" viewBox=\"0 -960 960 960\" width=\"24\"><path d=\"M70.31-187.694v-75.922q0-31.538 16.27-55.807 16.268-24.269 45.555-37.149 64.943-28.504 118.635-42.504 53.692-14 119.731-14 66.039 0 119.231 14 53.192 14 118.52 42.504 28.902 12.88 45.478 37.149 16.577 24.269 16.577 55.807v75.922H70.31Zm665.382 0v-74.768q0-53.384-24.864-89.069-24.863-35.685-64.98-58.391 53.23 7.615 101.152 20.807 47.922 13.193 80.167 29.74 28.369 15.914 45.446 40.898 17.077 24.985 17.077 56.015v74.768H735.692Zm-365.191-305.23q-57.942 0-95.413-37.471-37.47-37.471-37.47-95.412 0-57.942 37.47-95.221 37.471-37.278 95.413-37.278t95.22 37.278Q503-683.749 503-625.807q0 57.941-37.279 95.412-37.278 37.471-95.22 37.471Zm315.958-132.883q0 57.941-37.278 95.412-37.279 37.471-95.358 37.471-6.824 0-14.554-.923t-14.499-3.384q21.431-23.371 32.523-56.117 11.092-32.747 11.092-72.315 0-39.567-11.539-70.605-11.538-31.038-32.076-58.115 6.384-1.462 14.499-2.692 8.115-1.231 14.499-1.231 58.134 0 95.413 37.278 37.278 37.279 37.278 95.221ZM115.694-233.078h509.23v-30.538q0-16-9.116-30.038-9.115-14.039-26.192-22.347-62.769-29.307-110.615-40.5-47.846-11.192-108.5-11.192t-108.808 11.192q-48.154 11.193-110.922 40.5-17.077 8.308-26.077 22.347-9 14.038-9 30.038v30.538Zm254.615-305.229q37.461 0 62.384-24.924 24.923-24.923 24.923-62.384t-24.923-62.384q-24.923-24.923-62.384-24.923-37.462 0-62.385 24.923-24.923 24.923-24.923 62.384t24.923 62.384q24.923 24.924 62.385 24.924Zm0 305.229Zm0-392.537Z\"/></svg>",
  }),
]

export default class Client {
  modules = new MockProvider<Module>({url: "", mock: modulesMock});
}

export const client = new Client();

