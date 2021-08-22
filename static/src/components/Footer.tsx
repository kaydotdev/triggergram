import { useState } from 'react';
import {
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  IconButton,
  Tooltip,
  TooltipProps,
  Typography
} from '@material-ui/core';
import {
  AddCircleOutline as AddCircleOutlineIcon,
  Inbox as InboxIcon,
  FavoriteOutlined as FavoriteOutlinedIcon,
  PersonOutline as PersonOutlineIcon
} from '@material-ui/icons';
import GithubLogo from '../graphics/github-logo.svg';


type NavigationStates = "recents" | "favorite" | "profile" | "upload";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        appName: {
            fontWeight: 500,
            padding: '8px 0 0 12px',
            margin: 0
        },
        footer: {
            display: 'flex',
            justifyContent: 'center',
            margin: theme.spacing(3),
            width: 'calc(100% - 48px)',
            color: 'rgba(0, 0, 0, 0.87)',
            textAlign: 'center',
            backgroundColor: '#fafafa',
            borderColor: 'rgba(0, 0, 0, 0.30)',
            borderStyle: 'solid',
            borderRadius: '10px',
            borderWidth: '1px'
        },
        buttonLabel: {
            margin: '0 0 0 4px'
        }
    }),
);

const goToGithub = () => {
  window.location.href='https://github.com/antonAce/triggergram';
};

const useStylesTooltip = makeStyles((theme: Theme) => ({
  arrow: {
    color: theme.palette.common.black,
  },
  tooltip: {
    backgroundColor: theme.palette.common.black,
  },
}));

function BottomNavigationTooltip(props: TooltipProps) {
  const classes = useStylesTooltip();

  return <Tooltip arrow classes={classes} {...props} />;
}

function Footer() {
    const classes = useStyles();
    const [route, setRoute] = useState<NavigationStates>("recents");

    return (
      <footer className={classes.footer}>
          <BottomNavigationTooltip title="Recents" placement="top">
            <IconButton aria-label="recents" onClick={() => setRoute("recents")}>
              <InboxIcon />
              {
                route === "recents" ? (
                  <Typography className={classes.buttonLabel}
                              variant="button"
                              display="block"
                              gutterBottom>
                    RECENTS
                  </Typography>
                ) : (
                  <Typography />
                )
              }
            </IconButton>
          </BottomNavigationTooltip>
          <BottomNavigationTooltip title="Favorite" placement="top">
            <IconButton aria-label="favorite" onClick={() => setRoute("favorite")}>
              <FavoriteOutlinedIcon />
              {
                route === "favorite" ? (
                  <Typography className={classes.buttonLabel}
                              variant="button"
                              display="block"
                              gutterBottom>
                    FAVORITE
                  </Typography>
                ) : (
                  <Typography />
                )
              }
            </IconButton>
          </BottomNavigationTooltip>
          <BottomNavigationTooltip title="Profile" placement="top">
            <IconButton aria-label="profile" onClick={() => setRoute("profile")}>
              <PersonOutlineIcon />
              {
                route === "profile" ? (
                  <Typography className={classes.buttonLabel}
                              variant="button"
                              display="block"
                              gutterBottom>
                    PROFILE
                  </Typography>
                ) : (
                  <Typography />
                )
              }
            </IconButton>
          </BottomNavigationTooltip>
          <BottomNavigationTooltip title="Upload" placement="top">
            <IconButton aria-label="upload" onClick={() => setRoute("upload")}>
              <AddCircleOutlineIcon />
              {
                route === "upload" ? (
                  <Typography className={classes.buttonLabel}
                              variant="button"
                              display="block"
                              gutterBottom>
                    UPLOAD
                  </Typography>
                ) : (
                  <Typography />
                )
              }
            </IconButton>
          </BottomNavigationTooltip>
          <BottomNavigationTooltip title="Sources" placement="top">
            <IconButton aria-label="sources" onClick={goToGithub}>
              <img src={GithubLogo} alt="Triggergram project" width="22px" height="22px" />
            </IconButton>
          </BottomNavigationTooltip>
      </footer>
    );
}

export default Footer;
