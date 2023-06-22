import {matchPath, useLocation} from "react-router-dom";

export function usePath(pattern) {
  const { pathname } = useLocation();
  return matchPath(`/app/${pattern}/*`, pathname) !== null;
}