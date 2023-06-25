import {Box, SpeedDial, SpeedDialAction, Stack, Typography} from "@mui/material";
import {useEffect} from "react";
import InstancePath from "./InstancePath";
import {observer} from "mobx-react-lite";
import {Edit, Delete, Done, Close, ContentCopy} from "../icons";
import {action} from "mobx";

const InstanceHeader = observer(({form}) => {
  const data = form.data;

  useEffect(() => {
    document.title = `${data.title}`;
  })

  return (
    <Box display="flex" gap={2} sx={{background: "linear-gradient(96.34deg, #0FE9FF 0%, #19E7C1 51.56%, #0FFF8F 100%)"}}>
      <Box height={170} width={1} padding={2} display="flex" justifyContent="space-between" alignContent="center" flexDirection="column" sx={{background: "linear-gradient(180deg, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.4) 100%)"}}>
        <Box height={1} display="flex" justifyContent="space-between">
          <Box height={1} display="flex" gap={2}>
            <Box sx={{height: 138, width: 138, borderRadius: "8px", background: `url(${data.image}) no-repeat center`, backgroundSize: "cover"}}/>
            <Stack height={1} justifyContent="space-between" padding={1}>
              <InstancePath/>
              <Stack gap={1}>
                <Typography variant="h1">
                  {data.title}
                </Typography>
                <Typography variant="h4">
                  {data.subtitle}
                </Typography>
              </Stack>
            </Stack>
          </Box>
        </Box>
      </Box>
    </Box>
  );
});

export default InstanceHeader;