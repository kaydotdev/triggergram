import { useState, MouseEvent } from 'react';
import {
    AppBar,
    Toolbar,
    Button,
    MenuItem,
    Menu
} from '@material-ui/core';
import {
    AccountCircle as AccountCircleIcon
} from '@material-ui/icons';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';

import NotificationButton from './NotificationButton';
import MenuBanner from './MenuBanner';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        appbar: {
            color: '#fff',
            backgroundColor: '#000'
        },
        grow: {
            flexGrow: 1
        },
        appbarButton: {
            color: '#fff'
        }
    }),
);

function NavBar() {
    const classes = useStyles();

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleClick = (event: MouseEvent<HTMLButtonElement>) => { setAnchorEl(event.currentTarget); };
    const handleClose = () => { setAnchorEl(null); };

    return (
        <div className={classes.grow}>
            <AppBar className={classes.appbar} position="static">
                <Toolbar>
                    <MenuBanner />
                    <div className={classes.grow} />
                    <NotificationButton notificationCount={0} />
                    <Button
                        aria-label="profile"
                        className={classes.appbarButton}
                        aria-controls="profile-menu"
                        aria-haspopup="true"
                        onClick={handleClick}
                    >
                        <AccountCircleIcon />
                    </Button>
                    <Menu
                        id="profile-menu"
                        anchorEl={anchorEl}
                        keepMounted
                        open={Boolean(anchorEl)}
                        onClose={handleClose}
                    >
                        <MenuItem onClick={handleClose}>Profile</MenuItem>
                        <MenuItem onClick={handleClose}>Settings</MenuItem>
                        <MenuItem onClick={handleClose}>Logout</MenuItem>
                    </Menu>
                </Toolbar>
            </AppBar>
        </div>
    );
}

export default NavBar;