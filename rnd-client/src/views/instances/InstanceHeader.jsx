import {Box, Paper, Stack, Typography} from "@mui/material";
import {useEffect} from "react";
import InstancePath from "./InstancePath";

export default function InstanceHeader({data}) {
  useEffect(() => {
    document.title = `${data.title}`;
  })

  return (
    <Box display="flex" gap={2} sx={{background: "linear-gradient(96.34deg, #0FE9FF 0%, #19E7C1 51.56%, #0FFF8F 100%)"}}>
      <Box height={170} width={1} padding={2} display="flex" justifyContent="space-between" alignContent="center" flexDirection="column" sx={{background: "linear-gradient(180deg, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.4) 100%)"}}>
        <Box height={1} display="flex" gap={2}>
          <Paper sx={{height: 138, width: 138, borderRadius: "8px", background: `url(${data.image}) no-repeat center`, backgroundSize: "cover"}}>

          </Paper>
            {/*<img alt={title} src={image} style={{borderRadius: "8px"}}/>*/}
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
  );
}