// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.9;

contract MyContract {
    mapping(address => uint256) public playerScores;
    struct player {
        string name;
        uint256 score;
        uint256[] skins;
    }
    player[] public players;

    event ScoreAdded(address indexed)
}
