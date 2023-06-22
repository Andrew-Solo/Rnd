import {Box, Collapse, List, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {usePath} from "../../../hooks";
import Icon from "../../ui/Icon";
import {observer} from "mobx-react-lite";
import {useState} from "react";
import {ExpandLess, ExpandMore} from "../../icons";
import NavigationItem from "./NavigationItem";

const NavigationGroup = observer(({module}) => {
  const active = usePath(module.path);
  const [open, setOpen] = useState(false);

  const units = module.units;
  const {loaded, failed, message, data} = units;

  if (!loaded) return 'Loading...';
  if (failed) return message.title;

  if (data.length < 1) return (<></>);

  const expandable = data.length > 1;

  if (expandable) {
    return (
      <>
        <ListItemButton key={module.name} onClick={() => setOpen(!open)} sx={{padding: 2, gap: 1, background: active && !open ? "rgba(255, 255, 255, 0.1)" : null}}>
          <ListItemIcon sx={{minWidth: 0}}>
            <Icon icon={module.icon} color={active && !open ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
          </ListItemIcon>
          <ListItemText>
            <Box display="flex" justifyContent="space-between" alignItems="center">
              <Typography variant="h5" color={active && !open ? "primary" : "inherit"}>
                {module.title}
              </Typography>
              <Typography variant="caption" color="text.secondary" align="right" sx={{mb: -0.25}}>
                {module.subtitle}
              </Typography>
            </Box>
          </ListItemText>
          {open ? <ExpandLess/> : <ExpandMore/>}
        </ListItemButton>
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            {data.map(unit => (
              <NavigationItem key={unit.name} model={unit} nested/>
            ))}
          </List>
        </Collapse>
      </>
    );
  } else {
    return (
      <NavigationItem model={data[0]}/>
    );
  }
});

export default NavigationGroup