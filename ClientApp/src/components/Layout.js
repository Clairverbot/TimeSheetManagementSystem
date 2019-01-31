import React, { Component } from 'react';
import { TopMenu } from './TopMenu';

export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
          <div>
          <TopMenu/>
            <div 
            style={{ display: 'flex', justifyContent:'center', paddingLeft:210+'px',margin:2+'%'}}
            >
                {this.props.children}
            </div>
      </div>
    );
  }
}
