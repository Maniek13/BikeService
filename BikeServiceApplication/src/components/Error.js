import React, { Component } from 'react';
import {Text, View, StyleSheet} from 'react-native';

class Error extends Component {
    constructor(props){
        super(props);
    }

    render() {
        return (
            <View>
                <Text style={styles.text} name='state'>{this.props.error.message}</Text>             
            </View>
        );
    }
}

export default Error;

const styles = StyleSheet.create({
  text : {
    color: 'red',
    textAlign: 'center'
  }
});
