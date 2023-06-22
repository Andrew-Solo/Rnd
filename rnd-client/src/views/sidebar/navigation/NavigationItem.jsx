import {Box, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {usePath} from "../../../hooks";
import Icon from "../../ui/Icon";

const NavigationItem = ({model, nested = false}) => {
  const active = usePath(model.path);

  return (
    <ListItemButton key={model.name} href={model.path} sx={{padding: 2, gap: 1, background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
      <ListItemIcon sx={{minWidth: 0, ml: nested ? 4 : 0}}>
        <Icon icon={model.icon} color={active ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
      </ListItemIcon>
      <ListItemText>
        <Box display="flex" justifyContent="space-between" alignItems="center">
          <Typography variant="h5" color={active ? "primary" : "inherit"}>
            {model.title}
          </Typography>
          <Typography variant="caption" color="text.secondary" align="right" sx={{mb: -0.25}}>
            {model.subtitle}
          </Typography>
        </Box>
      </ListItemText>
    </ListItemButton>
  );
};

export default NavigationItem