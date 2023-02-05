import React, { Component } from 'react';
import {Text, View, StyleSheet, TouchableOpacity} from 'react-native';

import MainStyles from '../styles/MainStyles';
import Settings from '../objects/Settings';
import Error from '../components/Error';

class Popup extends Component {
    constructor(props){
        super(props);
        this.state = {
            showError: false,
            error: {},
            btnDisabled: false
        }
        this.handleAction = this.props.handleAction.bind(this);
        this.exitAction = this.props.exitAction.bind(this);
    }

    onError(error){
        this.setState({ error: error })
        this.setState({ showError: true })
        this.setState({ btnDisabled: false })
    }

    ok(){
        this.setState({ btnDisabled: true })
        this.handleAction( this.onError.bind(this))
    }

    exit(){
        this.exitAction()
    }

    render() {
        return (
            <View style={styles.popupConteiner}>
                <Text style={styles.header} name='state'>{this.props.header}</Text>
                <Text style={styles.message} name='state'>{this.props.message}</Text>
                <TouchableOpacity 
                    style={this.state.btnDisabled ? mainStyle.buttonDisabled : mainStyle.buttonEnabled } 
                    onPress = {this.ok.bind(this)}
                    disabled={this.state.btnDisabled}
                >
                    <Text>Tak</Text>
                </TouchableOpacity>
                <TouchableOpacity 
                    style={this.state.btnDisabled ? mainStyle.buttonDisabled : mainStyle.buttonEnabled } 
                    onPress = {this.exit.bind(this)}
                    disabled={this.state.btnDisabled}
                >
                    <Text>Wyjdź</Text>
                </TouchableOpacity>
                
                {this.state.showError == true ? <Error error = {this.state.error} /> : '' }
            </View>
        );
    }
}

export default Popup;

const mainStyle = MainStyles[Settings.SchemaStyle]
const styles = StyleSheet.create({
    message : {
        color: 'black',
        textAlign: 'center',
        fontSize: 12
    },
    header: {
        color: 'black',
        textAlign: 'center',
        fontWeight: 'bold',
        fontSize: 16
    },
    popupConteiner : {
        width: '100%',
        height: 'auto',
        position: 'absolute',
        top: '20%',
        backgroundColor: 'white',
        borderColor: 'black',
        borderWidth: 1,
        padding: 10,
        borderRadius: 10
    }
});

