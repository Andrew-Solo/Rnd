import {ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {matchPath, useLocation} from "react-router-dom";

export default function NavigationItem ({href, icon, title}) {
  const active = useRouteActive(href);
  const Icon = icon;

  return (
    <ListItemButton key={href} href={href} sx={{padding: "16px", background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
      <ListItemIcon>
        <Icon color={active ? "primary" : "inherit"}/>
      </ListItemIcon>
      <ListItemText>
        {/* TODO встроить эту тайпографи в ListItemText */}
        {/* TODO уменьшить расстояние от иконки до текста */}
        {/* TODO и сделать тут норм иконки */}
        <Typography variant="h5" color={active ? "primary" : "inherit"}>
          {title}
        </Typography>
      </ListItemText>
    </ListItemButton>
  );
}

function useRouteActive(pattern) {
  const { pathname } = useLocation();
  return matchPath(pattern, pathname) !== null;
}