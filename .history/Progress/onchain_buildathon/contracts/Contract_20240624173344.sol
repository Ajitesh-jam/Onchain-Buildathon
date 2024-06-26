// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.9;

contract MyContract {
    mapping(address => uint256) public playerScores;
    struct player {
        string name;
        address playerAddress;
        uint256 score;
        uint256[] skins;
    }
    player[] public players;

    event ScoreAdded(address indexed player, uint256 score);

    function isRegistered(address _player) private view returns (bool) {
        for (uint256 i = 0; i < players.length; i++) {
            if (address(uint160(players[i].playerAddress)) == _player) {
                return true;
            }
        }
        return false;
    }
}
