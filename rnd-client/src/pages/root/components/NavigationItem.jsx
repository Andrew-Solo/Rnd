import {Box, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {matchPath, useLocation} from "react-router-dom";

export default function NavigationItem ({href, icon, title, tip}) {
  const active = useRouteActive(href);
  const Icon = icon;

  return (
    <ListItemButton key={href} href={href} sx={{padding: 2, gap: 1, background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
      <ListItemIcon sx={{minWidth: 0}}>
        <Icon color={active ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
      </ListItemIcon>
      <ListItemText>
        <Box display="flex" justifyContent="space-between"  alignItems="flex-end">
          <Typography variant="h5" color={active ? "primary" : "inherit"}>
            {title}
          </Typography>
          <Typography variant="caption" color="text.secondary" align="right" sx={{mb: -0.25}}>
            {tip}
          </Typography>
        </Box>
      </ListItemText>
    </ListItemButton>
  );
}

function useRouteActive(pattern) {
  const { pathname } = useLocation();
  return matchPath(pattern, pathname) !== null;
}